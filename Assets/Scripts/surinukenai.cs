using UnityEngine;

public class surinukenai : MonoBehaviour
{
    [SerializeField] Transform _colliderObject; //���P�b�g�̃R���C�_�[���A�^�b�`����
    Vector3 _prePosition;
    float _minZ = 0;

    void Start()
    {
        //�����l��ۑ�
        _minZ = _colliderObject.localScale.z / 2;
        _prePosition = transform.position;
    }

    //�������Z�Ɋ֌W����̂�FixedUpdate��
    void FixedUpdate()
    {
        //�O�t���[������̕ψʂ��v�Z
        Vector3 def = transform.position - _prePosition;

        //�O�t���[������̕ψʂ����P�b�g�̐��ʕ���(transform.forward)�Ɏˉe���邱�ƂŁA���P�b�g���ǂꂾ���O��ɓ�������(defZ)���v�Z
        float defZ = Vector3.Dot(def, transform.forward);

        //���P�b�g���O��臒l(_minZ)�ȏ㓮�����ꍇ�A���邢�͌���臒l(_minZ)�ȏ㓮�����ꍇ�A�R���C�_�[�̈ʒu�ƃX�P�[�������ꂼ�꒲��
        if (defZ > _minZ)
        {
            _colliderObject.position = transform.position - (defZ - _minZ) / 2 * transform.forward;
            _colliderObject.localScale = new Vector3(_colliderObject.localScale.x, _colliderObject.localScale.y, defZ + _minZ);
        }
        else if (defZ < -_minZ)
        {

            _colliderObject.position = transform.position + (-defZ - _minZ) / 2 * transform.forward;
            _colliderObject.localScale = new Vector3(_colliderObject.localScale.x, _colliderObject.localScale.y, -defZ + _minZ);
        }
        else
        {
            _colliderObject.position = transform.position;
            _colliderObject.localScale = new Vector3(_colliderObject.localScale.x, _colliderObject.localScale.y, _minZ * 2);
        }

        //���݂̃��P�b�g�̈ʒu��ێ����Ă������t���[���ł̔�r�Ɏg�p
        _prePosition = transform.position;
    }

}
