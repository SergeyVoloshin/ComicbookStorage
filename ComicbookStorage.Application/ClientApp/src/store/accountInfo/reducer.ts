import { UpdateUserState, UpdateUserActionTypes, UPDATE_USER_SUCCESS, UPDATE_USER_ERROR, UPDATE_USER_RETRIEVE_CURRENT } from './types';

const initialState: UpdateUserState = {
    userData: undefined,
    errors: undefined,
}

export function updateUserReducer(
    state = initialState,
    action: UpdateUserActionTypes
): UpdateUserState {
    switch (action.type) {
        case UPDATE_USER_SUCCESS:
            return {
                ...state,
                errors: undefined,
            }
        case UPDATE_USER_ERROR:
            return {
                ...state,
                errors: action.errors,
            }
        case UPDATE_USER_RETRIEVE_CURRENT:
            return {
                ...state,
                errors: undefined,
                userData: action.userData
            }
        default:
            return state;
    }
}