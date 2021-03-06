﻿import React, { Component } from 'react';
import { render } from 'react-dom';
import Moment from 'moment';
import { UpVotes } from './UpVotes';

export class ListPostsComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            items: [],
            condition:false
        };
    }

    componentDidMount() {
        fetch("/api/PostsAPI")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        items: result
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
            )
    }

    render() {
        Moment.locale('en');
        const { error, isLoaded, items } = this.state;
        if (error) {
            return <div>Error: {error.message}</div>;
        } else if (!isLoaded) {
            return <div>Loading...</div>;
        } else {
            return (
                <div className="postcard-container">
                    {items.map(item => (
                        <div className="PostCard" key={item.postId}>
                            <div className="postCard-row">
                                <div className="postCard-column">
                                    <div className="PostCard-title">
                                        {item.title}
                                    </div>
                                    <div className="PostCard-content">
                                        {item.content}
                                    </div>
                                    <div className="PostCard-footer">
                                        <div className="PostCard-author">
                                            {item.author} - {Moment(item.publishTimeStamp).format('d MMM YYYY')}
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div className="postCard-column">
                                        <div className="postCard-flare" />
                                        <div className="postCard-upvote">
                                            <div className="upvoteIcon">
                                                <UpVotes item={item} /> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            );
        }
    }
}