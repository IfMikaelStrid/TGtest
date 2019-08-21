import React, { Component } from 'react';
import { render } from 'react-dom';
import PropTypes from 'prop-types';

export class Modal extends Component {
  render() {
    // Render nothing if the "show" prop is false
    if(!this.props.show) {
      return null;
    }

    // The gray background
    const backdropStyle = {
      position: 'fixed',
      top: 0,
      bottom: 0,
      left: 0,
      right: 0,
      backgroundColor: 'rgba(0,0,0,0.3)',
      padding: 50,
    };

    // The modal "window"
    const modalStyle = {
      backgroundColor: '#111111',
      borderRadius: 5,
      maxWidth: 500,
      height: 300,
      margin: '0 auto',
      padding: 35,
      display: 'flex',
      top: '20%'
    };

    var contentStyle = {
        'alignitems': 'center',
        justifyContent: 'center',
        width: '100%'
    };

    var closeButtonStyle = {
        right: 0,
        top: 0,
        position: 'absolute',
        border: 'none',
        borderRadius: '50%',
        margin: '10px 10px 10px 0',
        backgroundColor: '#333',
        color: '#fff',
    };


    return (
      <div className="backdrop" style={backdropStyle}>
            <div className="modal" style={modalStyle} >
                <div style={contentStyle}>
                    {this.props.children}
                </div>
            <div className="footer">
            <button style={closeButtonStyle} onClick={this.props.onClose} >
                    ✖ 
            </button>
          </div>
        </div>
      </div>
    );
  }
}

Modal.propTypes = {
  onClose: PropTypes.func.isRequired,
  show: PropTypes.bool,
  children: PropTypes.node
};
