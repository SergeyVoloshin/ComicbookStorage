import React, { Component } from "react";
import { bindActionCreators, Dispatch } from 'redux';
import { connect } from 'react-redux';


export class AddComicbook extends Component {
    render() {
        return (<div>In Progress...</div>);
    }
}

export default connect()(AddComicbook)
