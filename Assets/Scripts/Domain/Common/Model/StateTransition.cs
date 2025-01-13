using System;
using System.Collections.Generic;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    public enum State {
        /// <summary>審査中</summary>
        UnderReview,

        /// <summary>承認中</summary>
        Approved,

        /// <summary>実施中</summary>
        InProgress,

        /// <summary>終了</summary>
        Completed,

        /// <summary>差し戻し</summary>
        Rejected,

        /// <summary>中断中</summary>
        Interrupted,
    }


    public sealed class StateTransition {

        private readonly Dictionary<State, HashSet<State>> _allowedTransitions;

        // コンストラクタで状態遷移テーブルを初期化
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
        /// 指定された状態遷移が可能か確認
        /// </summary>
        public bool CanTransit(State from, State to) {
            return _allowedTransitions.TryGetValue(from, out var allowedStates) &&
                   allowedStates.Contains(to);
        }

        /// <summary>
        /// 指定された状態から遷移可能な次の状態を取得
        /// </summary>
        public IEnumerable<State> GetNextStates(State currentState) {
            if (_allowedTransitions.TryGetValue(currentState, out var allowedStates)) {
                return allowedStates;
            }
            return Array.Empty<State>();
        }
    }
}
