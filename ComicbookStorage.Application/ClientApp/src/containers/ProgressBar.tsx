import React, { Component } from 'react';
import TopBarProgress from 'react-topbar-progress-indicator';
import { connect } from 'react-redux';
import { ApplicationState } from '../store/configureStore';


TopBarProgress.config({
    barThickness: 3
});

type ProgressBarProps = ReturnType<typeof mapStateToProps>;


export class ProgressBar extends Component<ProgressBarProps> {

    constructor(props: ProgressBarProps) {
        super(props);
    }
    render() {

        if (this.props.uiState.isInProgress) {
            return (<TopBarProgress />)
        } else {
            return ('');
        }

    }
}

const mapStateToProps = (state: ApplicationState) => {
    return { uiState: state.commonUi }
}

export default connect(
    mapStateToProps,
    null
)(ProgressBar)