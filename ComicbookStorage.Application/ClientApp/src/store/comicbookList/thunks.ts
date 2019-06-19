import { Dispatch } from 'redux';
import { ComicbookListReceivedAction, ComicbookListDto } from './types';
import { receiveComicbookList } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const requestComicbookListAsync = (pageNumber: number, pageSize: number) => (dispatch: Dispatch): Promise<ComicbookListReceivedAction> => {
    return comicbookServer.get(`/comicbook/get-page/${pageNumber}/${pageSize}`, false, false)
        .then(response => response.json())
        .then(json => dispatch(receiveComicbookList(json)));
}