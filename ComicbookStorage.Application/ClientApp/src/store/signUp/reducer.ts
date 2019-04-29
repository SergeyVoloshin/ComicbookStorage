import { CreateUserState, CreateUserActionTypes, CREATE_USER_RESPONSE, CREATE_USER_ERROR } from './types';

const initialState: CreateUserState = {
    created: false,
}

export function createUserReducer(
    state = initialState,
    action: CreateUserActionTypes
): CreateUserState {
    switch (action.type) {
        case CREATE_USER_RESPONSE:
            return {
                ...state,
                created: true
            }
        case CREATE_USER_ERROR:
            return {
                ...state,
                created: false
            }
        default:
            return state;
    }
}