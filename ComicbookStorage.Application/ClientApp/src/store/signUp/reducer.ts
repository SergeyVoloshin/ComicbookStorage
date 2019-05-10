import { CreateUserState, CreateUserActionTypes, CREATE_USER_RESPONSE, CREATE_USER_ERROR } from './types';

const initialState: CreateUserState = {
    errors: undefined,
}

export function createUserReducer(
    state = initialState,
    action: CreateUserActionTypes
): CreateUserState {
    switch (action.type) {
        case CREATE_USER_RESPONSE:
            return {
                ...state,
                errors: undefined
            }
        case CREATE_USER_ERROR:
            return {
                ...state,
                errors: action.errors
            }
        default:
            return state;
    }
}