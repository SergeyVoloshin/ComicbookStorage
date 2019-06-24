import { RestoreState, RestoreAccessActionTypes, RESTORE_SUCCESS, RESTORE_ERROR } from './types';

const initialState: RestoreState = {
    errors: undefined,
}

export function restoreAccessReducer(
    state = initialState,
    action: RestoreAccessActionTypes
): RestoreState {
    switch (action.type) {
        case RESTORE_SUCCESS:
            return {
                ...state,
                errors: undefined,
            }
        case RESTORE_ERROR:
            return {
                ...state,
                errors: action.errors,
            }
        default:
            return state;
    }
}