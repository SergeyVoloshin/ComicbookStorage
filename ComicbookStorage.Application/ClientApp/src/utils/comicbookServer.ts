import fetch from 'cross-fetch';
import { store } from '..';
import { setProgressBar } from '../store/commonUi/actions';

class ComicbookServer {
    get<T>(url: string): Promise<T> {
        store.dispatch(setProgressBar(true));

        return fetch(url)
            .then(
                response => {
                    store.dispatch(setProgressBar(false));
                    return response.json();
                },
                error => console.log('An error occurred.', error)
            );
    }
}

const comicbookServer = new ComicbookServer();

export default comicbookServer