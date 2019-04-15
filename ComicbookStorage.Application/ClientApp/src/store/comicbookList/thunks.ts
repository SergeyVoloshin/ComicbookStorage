import { Dispatch } from 'redux';
import { ComicbookListActionTypes, ComicbookListItemDto } from './types';
import { receiveComicbookList } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const requestComicbookListAsync = () => (dispatch: Dispatch): Promise<ComicbookListActionTypes> => {
    return comicbookServer.get<ComicbookListItemDto[]>('/api/comicbook')
        .then(json => dispatch(receiveComicbookList(json)));
}