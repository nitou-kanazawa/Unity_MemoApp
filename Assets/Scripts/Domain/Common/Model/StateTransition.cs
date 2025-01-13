using System;
using System.Collections.Generic;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    public enum State {
        /// <summary>�R����</summary>
        UnderReview,

        /// <summary>���F��</summary>
        Approved,

        /// <summary>���{��</summary>
        InProgress,

        /// <summary>�I��</summary>
        Completed,

        /// <summary>�����߂�</summary>
        Rejected,

        /// <summary>���f��</summary>
        Interrupted,
    }


    public sealed class StateTransition {

        private readonly Dictionary<State, HashSet<State>> _allowedTransitions;

        // �R���X�g���N�^�ŏ�ԑJ�ڃe�[�u����������
        public StateTransition() {
            // 
            _allowedTransitions = new Dictionary<State, HashSet<State>>{
                { State.UnderReview, new HashSet<State> { State.Approved, State.Rejected } },
                { State.Approved, new HashSet<State> { State.InProgress, State.Rejected } },
                { State.InProgress, new HashSet<State> { State.Completed } },
                { State.Rejected, new HashSet<State> { State.UnderReview} },
            };
        }

        /// <summary>
        /// �w�肳�ꂽ��ԑJ�ڂ��\���m�F
        /// </summary>
        public bool CanTransit(State from, State to) {
            return _allowedTransitions.TryGetValue(from, out var allowedStates) &&
                   allowedStates.Contains(to);
        }

        /// <summary>
        /// �w�肳�ꂽ��Ԃ���J�ډ\�Ȏ��̏�Ԃ��擾
        /// </summary>
        public IEnumerable<State> GetNextStates(State currentState) {
            if (_allowedTransitions.TryGetValue(currentState, out var allowedStates)) {
                return allowedStates;
            }
            return Array.Empty<State>();
        }
    }
}
