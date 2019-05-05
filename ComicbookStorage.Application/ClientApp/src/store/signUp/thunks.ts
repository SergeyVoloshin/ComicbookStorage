import { Dispatch } from 'redux';
import { CreateUserActionTypes } from './types';
import { processUserCreated } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const createUserAsync = () => (dispatch: Dispatch): Promise<CreateUserActionTypes> => {
    return new Promise(resolve => setTimeout(resolve, 1000)).then(res => dispatch(processUserCreated()));
}

export const isNameTakenAsync = (name: string): Promise<boolean> => {
    return comicbookServer.get<boolean>('/account/is-name-taken/' + name, false)
        .then(result => {
            if (result) {
                throw { name: 'That username is taken' }
            }
            return false;
        });
}