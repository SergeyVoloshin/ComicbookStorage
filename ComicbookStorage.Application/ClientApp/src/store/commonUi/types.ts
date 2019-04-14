export interface CommonUiState {
    isInProgress: boolean
}

export const SET_PROGRESS_BAR = 'SET_PROGRESS_BAR';

interface SetProgressBarAction {
    type: typeof SET_PROGRESS_BAR,
    isInProgress: boolean
}

export type CommonUiActionTypes = SetProgressBarAction