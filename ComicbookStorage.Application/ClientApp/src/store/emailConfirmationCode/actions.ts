import { EmailConfirmedAction, CONFIRM_EMAIL_SUCCESS, ConfirmEmailFailedAction, CONFIRM_EMAIL_ERROR } from './types';
import { ErrorResponse } from '../common/types';

export function processEmailConfirmed(): EmailConfirmedAction {
    return {
        type: CONFIRM_EMAIL_SUCCESS,
    }
}

export function processConfirmEmailFailed(errorResponse: ErrorResponse): ConfirmEmailFailedAction {
    return {
        type: CONFIRM_EMAIL_ERROR,
        errors: errorResponse.errors,
    }
}