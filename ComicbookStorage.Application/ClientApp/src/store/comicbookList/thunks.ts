import { Dispatch } from 'redux';
import { ComicbookListActionTypes, ComicbookListDto } from './types';
import { receiveComicbookList } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const requestComicbookListAsync = (pageNumber: number) => (dispatch: Dispatch): Promise<ComicbookListActionTypes> => {
    return comicbookServer.get<ComicbookListDto>('/api/comicbook/' + pageNumber + '/' + 50)
        .then(json => dispatch(receiveComicbookList(json)));
}