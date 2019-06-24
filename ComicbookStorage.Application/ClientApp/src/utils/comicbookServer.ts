import fetch from 'cross-fetch';
import { store } from '..';
import { setProgressBar } from '../store/commonUi/actions';
import { logOut } from '../store/logIn/actions';


class ComicbookServer {
    get(url: string, sendAuthenticationToken: boolean, showProgressbar: boolean): Promise<Response> {
        return sendRequest(url, null, "GET", sendAuthenticationToken, showProgressbar);
    }

    post<T>(url: string, data: T, sendAuthenticationToken: boolean, showProgressbar: boolean): Promise<Response> {
        return sendRequest(url, data, "POST", sendAuthenticationToken, showProgressbar);
    }

    put<T>(url: string, data: T, sendAuthenticationToken: boolean, showProgressbar: boolean): Promise<Response> {
        return sendRequest(url, data, "PUT", sendAuthenticationToken, showProgressbar);
    }

    setAuthenticationTokens(tokens: AuthenticationResponseDto) {
        setAuthenticationTokens(tokens);
    }

    clearAuthenticationTokens() {
        localStorage.removeItem(accessTokenKey);
        localStorage.removeItem(refreshTokenKey);
    }

    isAuthenticated() {
        return localStorage.getItem(accessTokenKey) !== null;
    }
}

const getAuthenticationHeader = () => {
    let token: string | null = getAccessToken();
    if (token) {
        return { "Authorization": `Bearer ${token}` }
    }
    return undefined;
}

const getRefreshToken = () => {
    return localStorage.getItem(refreshTokenKey);
}

const getAccessToken = () => {
    return localStorage.getItem(accessTokenKey);
}

const setAuthenticationTokens = (tokens: AuthenticationResponseDto) => {
    localStorage.setItem(accessTokenKey, tokens.accessToken);
    localStorage.setItem(refreshTokenKey, tokens.refreshToken);
}

interface AuthenticationResponseDto {
    accessToken: string,
    refreshToken: string,
}

const bodyTypeHeaders: HeadersInit = {
    "Accept": "application/json",
    "Content-Type": "application/json"
}

const accessTokenKey: string = "accessToken";
const refreshTokenKey: string = "refreshToken";

const sendRequest = async <T>(url: string, data: T, method: string, sendAuthenticationToken: boolean, showProgressbar: boolean): Promise<Response> => {
    checkProgressStart(showProgressbar);

    let request: RequestInit = {
        method: method,
        body: data ? JSON.stringify(data) : null,
        headers: {}
    }

    if (sendAuthenticationToken) {
        request.headers = {
            ...request.headers,
            ...getAuthenticationHeader()
        }
    }

    
    if (data) {
        request.headers = {
            ...request.headers,
            ...bodyTypeHeaders
        }
    }

    let response: Response = await fetch(url, request);

    if (sendAuthenticationToken && response.status === 401) {
        let refreshToken = getRefreshToken();
        let accessToken = getAccessToken();
        let refreshResponse: Response = await fetch(
            'account/refresh-token',
            {
                method: 'POST',
                headers: bodyTypeHeaders,
                body: JSON.stringify({ accessToken: accessToken, refreshToken: refreshToken }),
            });
        if (!refreshResponse.ok) {
            checkProgressStop(showProgressbar);
            store.dispatch(logOut());
            return refreshResponse;
        }
        setAuthenticationTokens(await refreshResponse.json());
        let result: Response = await sendRequest(url, data, method, sendAuthenticationToken, false);
        checkProgressStop(showProgressbar);
        return result;
    }

    checkProgressStop(showProgressbar);
    return response;
}

const checkProgressStart = (showProgressbar: boolean) => {
    if (showProgressbar) {
        store.dispatch(setProgressBar(true));
    }
}

const checkProgressStop = (showProgressbar: boolean) => {
    if (showProgressbar) {
        store.dispatch(setProgressBar(false));
    }
}

const comicbookServer = new ComicbookServer();

export default comicbookServer