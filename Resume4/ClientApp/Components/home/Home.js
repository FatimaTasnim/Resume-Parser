import React, { Component } from 'react';
import PropTypes from 'prop-types';
import PreviewSection from './PreviewSection';
import FileManagement from './FileManagement';

export default class Home extends Component {

    constructor(props) {
        super(props);
        this.state = {
            error: null,
            previewText: "test text initial",
            isLoaded: false,
            items: []
        };
        this.onTextChange = this.onTextChange.bind(this);
        this.FindTheComponent = this.FindTheComponent.bind(this);
    }
    onTextChange(previewText) {
        fetch(`https://localhost:44302/api/resumes/GetFileData/${previewText}`)
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ previewText: result.text });
                },
                // Note: it's important to handle errors here
                // instead of a catch() block so that we don't swallow
                // exceptions from actual bugs in components.
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
            )
    }


    componentDidMount() {
        fetch("https://localhost:44302/api/Resumes/GetFileNames")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        items: result
                    });
                },
                // Note: it's important to handle errors here
                // instead of a catch() block so that we don't swallow
                // exceptions from actual bugs in components.
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
            )
    }

    FindTheComponent(filename,Key) {
        fetch(`https://localhost:44302/api/resumes/SearchResult/${filename}/${Key}`, { method: 'post' })
            .then(res => res.json())
            .then(
                (result) => {
                    console.log(result);
                    console.log(filename);
                    this.setState({ previewText: result.htmlText });
                },
                // Note: it's important to handle errors here
                // instead of a catch() block so that we don't swallow
                // exceptions from actual bugs in components.
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
                <div class="body">
                    <div class="items-section">
                        <FileManagement onTextChange={this.onTextChange} FindTheComponent={this.FindTheComponent} state={this.state} />
                        <div class="section-separator"></div>
                        <PreviewSection previewText={this.state.previewText} />
                    </div>
                </div>
            );
        }
    }                                                              
}

Home.propTypes = {
    notify: PropTypes.func,
};
