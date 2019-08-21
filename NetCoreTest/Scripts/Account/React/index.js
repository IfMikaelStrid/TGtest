import React from 'react';
import { render } from 'react-dom';
import { MyPosts } from './Components/ShowMyPosts';

const AccountApp = () => (
    <>
        <div>  
            <MyPosts />
        </div>
    </>
);

render(<AccountApp />, document.getElementById('AccountApp'));