import React, { Component } from 'react';
import { render } from 'react-dom';
import Moment from 'moment';
import { EditPost } from './EditPost';
import { DeletePost } from './DeletePost'; 

export class MyPosts extends Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            items: [],
            condition: false,
            edit: false,
            deletePost: false,
            editPostId: "",
            postTitle: ""
        };

        this.editPost = this.editPost.bind(this);
        this.deletePost = this.deletePost.bind(this);
    }

    componentDidMount() {
        fetch("/getAllUserPosts")
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

    editPost(postId) {
        this.setState({
            edit: true,
            editPostId: postId
        });
    }
    deletePost(postId, title) {
        this.setState({
            editPostId: postId,
            postTitle:  title,
            deletePost: true
        });
    }

    render() {

        Moment.locale('en');
        const { error, isLoaded, items, edit, editPostId, deletePost, postTitle} = this.state;
        if (error) {
            return <div>Error: {error.message}</div>;
        } else if (!isLoaded) {
            return <div>Loading...</div>;
        }
        else if (edit) {
            return (
                <EditPost editPostId={editPostId} />
            );
        } else if (deletePost) {
            return (
                <DeletePost editPostId={editPostId} postTitle={postTitle}/>
            );
        }
        else {
            return (
                <div className="postcard-container">
                    {items.map(item => (
                        <div className="PostCard PostCard-myPosts" key={item.postId}>
                            <div className="postCard-row">
                                <div className="postCard-column">
                                    <div className="PostCard-title">
                                        {item.title}
                                    </div>
                                    <div className="PostCard-content PostCard-clamper">
                                        {item.content}
                                    </div>
                                    <div className="PostCard-footer">
                                        <div className="PostCard-author">
                                            {item.author} - {Moment(item.publishTimeStamp).format('d MMM YYYY')}
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div className="postCard-column postCard-buttonContainer">
                                        <div onClick={() => this.editPost(item.postId)}>
                                            <i className="fas fa-edit" />
                                        </div>
                                        <div onClick={() => this.deletePost(item.postId, item.title)}>
                                            <i className="fas fa-trash-alt"/>
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