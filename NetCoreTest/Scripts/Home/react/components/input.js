import React, { Component } from 'react';

var inputStyle = {
    width: '100%'
};

const Input = (props) => {
    return (  
<div className="form-group">
    <div>
        <input
          className="form-input"
          id={props.name}
          name={props.name}
          type={props.type}
          value={props.value}
          placeholder={props.placeholder} 
          style={inputStyle}
           />
    </div>
  </div>
)
}

export default Input;