export interface User {
    email: string,
    name: string,
    password: string,
}

export interface CreateUserState {
    created: boolean
}

export const CREATE_USER_RESPONSE = 'CREATE_USER_RESPONSE';

export const CREATE_USER_ERROR = 'CREATE_USER_ERROR';

export interface UserCreatedAction {
    type: typeof CREATE_USER_RESPONSE,
}

export interface CreateUserFailedAction {
    type: typeof CREATE_USER_ERROR,
}

export type CreateUserActionTypes = UserCreatedAction | CreateUserFailedAction