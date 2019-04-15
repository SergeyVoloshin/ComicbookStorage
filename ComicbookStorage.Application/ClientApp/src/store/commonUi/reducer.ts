import { CommonUiState, CommonUiActionTypes, SET_PROGRESS_BAR } from './types';

const initialState: CommonUiState = {
    isInProgress: false,
}

export function progressBarReducer(
    state = initialState,
    action: CommonUiActionTypes
): CommonUiState {
    switch (action.type) {
        case SET_PROGRESS_BAR:
            return Object.assign({}, state, {
                isInProgress: action.isInProgress,
            });
        default:
            return state;
    }
}