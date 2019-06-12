import { LogInState, LogInActionTypes, LOG_IN_SUCCESS, LOG_IN_ERROR, LOG_OUT } from './types';

const initialState: LogInState = {
    authenticated: false,
    errors: undefined,
}

export function logInReducer(
    state = initialState,
    action: LogInActionTypes
): LogInState {
    switch (action.type) {
        case LOG_IN_SUCCESS:
        return {
            ...state,
            authenticated: true,
            errors: undefined,
            }
        case LOG_OUT:
            return {
                ...state,
                authenticated: false,
                errors: undefined,
            }
        case LOG_IN_ERROR:
        return {
            ...state,
            errors: action.errors,
        }
    default:
        return state;
    }
}