import { ValidationErrors } from '../common/types';

export interface ConfirmEmailState {
    errors?: ValidationErrors,
}

export const CONFIRM_EMAIL_SUCCESS = 'CONFIRM_EMAIL_SUCCESS';

export const CONFIRM_EMAIL_ERROR = 'CONFIRM_EMAIL_ERROR';

export interface EmailConfirmedAction {
    type: typeof CONFIRM_EMAIL_SUCCESS,
}

export interface ConfirmEmailFailedAction {
    type: typeof CONFIRM_EMAIL_ERROR,
    errors: ValidationErrors,
}

export type ConfirmEmailActionTypes = EmailConfirmedAction | ConfirmEmailFailedAction