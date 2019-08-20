import React from 'react';
import { render } from 'react-dom';
import { ListPostsComponent } from '../../Home/react/components/listPosts';
import { NewPost } from '../../Home/react/components/NewPost';

const App = () => (
    <>
        <ListPostsComponent />
        <NewPost />
    </>
);

render(<App />, document.getElementById('app'));