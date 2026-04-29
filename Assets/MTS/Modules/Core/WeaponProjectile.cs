using Enemies;
using UnityEngine;

namespace MTS.Modules.Core
{
    [CreateAssetMenu(menuName = "Towers/Modules/Weapons/Projectile")]
    public class WeaponProjectile : ScriptableObject
    {
        public GameObject projectilePrefab;
        public float speed;
        public float lifetime;
        public Vector3 rotationOffsetEuler;

        public bool IsValid => projectilePrefab;

        public void Spawn(Transform origin, Enemy target)
        {
            if (!IsValid || !origin || !target) return;

            var startPosition = origin.position;
            var targetPosition = target.transform.position;
            var direction = targetPosition - startPosition;
            var rotation = direction.sqrMagnitude > Mathf.Epsilon
                ? GetTravelRotation(direction.normalized, origin.up)
                : origin.rotation;

            var projectileInstance = Instantiate(projectilePrefab, startPosition, rotation);
            var controller = projectileInstance.GetComponent<WeaponProjectileInstance>();
            if (!controller)
            {
                controller = projectileInstance.AddComponent<WeaponProjectileInstance>();
            }

            controller.Initialise(target, speed, lifetime, rotationOffsetEuler);
        }

        private Quaternion GetTravelRotation(Vector3 direction, Vector3 up)
        {
            return Quaternion.LookRotation(direction, up) * Quaternion.Euler(rotationOffsetEuler);
        }
    }

    public class WeaponProjectileInstance : MonoBehaviour
    {
        [SerializeField] private float hitDistance = 0.1f;

        private Enemy _target;
        private Vector3 _targetPosition;
        private float _speed;
        private float _lifeRemaining;
        private Vector3 _rotationOffset;

        public void Initialise(Enemy target, float projectileSpeed, float projectileLifetime, Vector3 rotationOffsetEuler = default)
        {
            _target = target;
            _speed = Mathf.Max(0.01f, projectileSpeed);
            _lifeRemaining = Mathf.Max(0.01f, projectileLifetime);
            _targetPosition = target ? target.transform.position : transform.position;
            _rotationOffset = rotationOffsetEuler;
        }

        private void Update()
        {
            _lifeRemaining -= Time.deltaTime;
            if (_lifeRemaining <= 0f)
            {
                Destroy(gameObject);
                return;
            }

            if (_target)
            {
                _targetPosition = _target.transform.position;
            }

            var toTarget = _targetPosition - transform.position;
            var travelDistance = _speed * Time.deltaTime;

            if (toTarget.sqrMagnitude <= hitDistance * hitDistance || toTarget.magnitude <= travelDistance)
            {
                transform.position = _targetPosition;
                Destroy(gameObject);
                return;
            }

            var direction = toTarget.normalized;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(_rotationOffset);
            transform.position += direction * travelDistance;
        }
    }
}
