import React from 'react';
import { Route } from 'react-router';
import { NotificationContainer } from 'react-notifications';
import 'react-notifications/lib/notifications.css';
import Layout from './components/Layout';
import Home from './components/Home';
import ComicbookList from './containers/ComicbookList';
import ProgressBar from './containers/ProgressBar';
import CheckEmailConfirmationCode from './containers/CheckEmailConfirmationCode';
import LogIn from './containers/LogIn';
import SignUp from './containers/SignUp';
import ConfirmEmail from './containers/ConfirmEmail';
import AppPathConfig from './utils/appPathConfig';

export default () => (
    <div>
        <ProgressBar />
        <NotificationContainer />
        <Layout>
            <Route exact path={AppPathConfig.home} component={Home} />
            <Route path={AppPathConfig.comicbooks} component={ComicbookList} />
            <Route path={AppPathConfig.confirmEmail} component={ConfirmEmail} />
            <Route path={AppPathConfig.sendEmailConfirmationCode} component={CheckEmailConfirmationCode} />
            <Route path={AppPathConfig.signUp} component={SignUp} />
            <Route path={AppPathConfig.logIn} component={LogIn} />
        </Layout>
    </div>
);
