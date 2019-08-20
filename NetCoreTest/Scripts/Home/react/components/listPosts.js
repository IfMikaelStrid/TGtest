import React, { Component } from 'react';
import { render } from 'react-dom';

export class ListPostsComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      error: null,
      isLoaded: false,
      items: []
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
    const { error, isLoaded, items } = this.state;
    if (error) {
      return <div>Error: {error.message}</div>;
    } else if (!isLoaded) {
      return <div>Loading...</div>;
    } else {
      return (
        <div>
           {items.map(item => (
           <div className="PostCard" key={item.postId}>
                  <div>
                      {item.title}
                  </div>
                  <div>
                      {item.content}
                  </div>
                  <div>
                      {item.author} {item.publishTimeStamp}
                  </div>
            </div>
          ))}
        </div>
      );
    }
  }
}