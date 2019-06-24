import { EmailSentAction, RESTORE_SUCCESS, RESTORE_ERROR, RestoreFailedAction } from './types';
import { ErrorResponse } from '../common/types';

export function processEmailSent(): EmailSentAction {
    return {
        type: RESTORE_SUCCESS,
    }
}

export function processRestoreFailed(errorResponse: ErrorResponse): RestoreFailedAction {
    return {
        type: RESTORE_ERROR,
        errors: errorResponse.errors,
    }
}