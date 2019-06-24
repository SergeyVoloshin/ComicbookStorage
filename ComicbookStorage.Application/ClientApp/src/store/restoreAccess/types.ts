import { ValidationErrors } from '../common/types';

export interface RestoreRequest {
    email: string,
}

export interface RestoreDto {
    email: string,
}

export interface RestoreState {
    errors?: ValidationErrors,
}

export const RESTORE_SUCCESS = 'RESTORE_SUCCESS';

export const RESTORE_ERROR = 'RESTORE_ERROR';

export interface EmailSentAction {
    type: typeof RESTORE_SUCCESS,
}

export interface RestoreFailedAction {
    type: typeof RESTORE_ERROR,
    errors: ValidationErrors,
}

export type RestoreAccessActionTypes = EmailSentAction | RestoreFailedAction;