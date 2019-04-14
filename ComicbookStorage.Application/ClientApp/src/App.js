import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import TileGrid from './components/TileGrid/TileGrid';
import ComicbookList from './containers/ComicbookList';

const Tiles1 = [
    { id: 1, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 1', description: 'Some comicbook description 1' },
    { id: 2, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 2', description: 'Some comicbook description 2' },
    { id: 3, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 3', description: 'Some comicbook description 3' },
    { id: 4, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 1', description: 'Some comicbook description 1' },
    { id: 5, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 2', description: 'Some comicbook description 2' },
    { id: 6, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 1', description: 'Some comicbook description 1' },
    { id: 7, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 2', description: 'Some comicbook description 2' },
    { id: 8, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 3', description: 'Some comicbook description 3' },
    { id: 9, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 1', description: 'Some comicbook description 1' },
    { id: 10, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 2', description: 'Some comicbook description 2' },
    { id: 11, url: '#', imageUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', title: 'test comicbook title 3', description: 'Some comicbook description 3' }
];

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        <Route path='/comicbooks' component={ComicbookList} />
        <Route exact path={"/test"} component={() => <TileGrid tiles={Tiles1}/>} />

    </Layout>
);
