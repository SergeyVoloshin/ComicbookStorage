import { Dispatch } from 'redux';
import { CreateUserActionTypes } from './types';
import { processUserCreated } from './actions';

export const createUserAsync = () => (dispatch: Dispatch): Promise<CreateUserActionTypes> => {
    console.log('123');
    return new Promise(resolve => setTimeout(resolve, 1000)).then(res => dispatch(processUserCreated()));
}