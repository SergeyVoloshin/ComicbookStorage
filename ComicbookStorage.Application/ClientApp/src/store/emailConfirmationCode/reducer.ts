import { ConfirmEmailState, ConfirmEmailActionTypes, CONFIRM_EMAIL_SUCCESS, CONFIRM_EMAIL_ERROR } from './types';

const initialState: ConfirmEmailState = {
    errors: undefined,
}

export function confirmEmailReducer(
    state = initialState,
    action: ConfirmEmailActionTypes
): ConfirmEmailState {
    switch (action.type) {
        case CONFIRM_EMAIL_SUCCESS:
            return {
                ...state,
                errors: undefined,
            }
        case CONFIRM_EMAIL_ERROR:
            return {
                ...state,
                errors: action.errors,
            }
        default:
            return state;
    }
}