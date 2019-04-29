import { UserCreatedAction, CREATE_USER_RESPONSE, CreateUserFailedAction, CREATE_USER_ERROR } from './types';

export function processUserCreated(): UserCreatedAction {
    return {
        type: CREATE_USER_RESPONSE,
    }
}

export function processCreateUserFailed(): CreateUserFailedAction {
    return {
        type: CREATE_USER_ERROR,
    }
}