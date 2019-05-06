import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import ComicbookList from './containers/ComicbookList';
import ProgressBar from './containers/ProgressBar';
import SignUp from './containers/SignUp';

export default () => (
    <div>
        <ProgressBar />
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/comicbooks' component={ComicbookList} />
            <Route path='/sign-up' component={SignUp} />
        </Layout>
    </div>
);
