import { CommonUiActionTypes, SET_PROGRESS_BAR } from './types';

export function setProgressBar(isInProgress: boolean): CommonUiActionTypes {
    return {
        type: SET_PROGRESS_BAR,
        isInProgress: isInProgress
    }
}