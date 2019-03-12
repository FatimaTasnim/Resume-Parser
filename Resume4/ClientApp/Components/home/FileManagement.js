import React, { Component } from 'react';
import PropTypes from 'prop-types';



export default class FileManagement extends Component {

    constructor(props) {
        super(props);
        this.onHover = this.onHover.bind(this);
        this.onSearch = this.onSearch.bind(this);
        this.state = { value: 'tests' };
        this.hovState = { value: 'call.docx' };
        //this.hovs = { value: "risk" };
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(e) {
        this.setState({ value: e.target.value });
    }
    onHover(index) {
        console.log(index);
        this.hovState.value = index.currentTarget.getElementsByTagName('a')[0].text;
        this.props.onTextChange(index.currentTarget.getElementsByTagName('a')[0].text);
        // props.state.setPreviewText();
    }
    onSearch(e, file, input) {
        e.preventDefault();
        console.log(file);
        this.props.FindTheComponent(file,input);

        console.log(input);
    }
    render() {
        return (
            <div class="File-management">
                <h2>File Managemnet Page</h2>
                <form onSubmit={this.formPreventDefault}>
                    <input class="upload-file-name-input"
                        type="text"
                        name="searchkey"
                        //// <button onClick ={this.handleIncrement} className="btn btn-secondary btn-sm">Increment</button>
                        onChange={this.handleChange} />
                    <button class="upload-btn-input" onClick={(e) => this.onSearch(e, this.hovState.value, this.state.value)}  >Submit</button>
                    <div class="file-list-serction">

                        <ul>
                            {this.props.state.items.map(item => (
                                <li key={item} onMouseOver={this.onHover}>
                                    <a>{item}</a>
                                    <span class="cross">X</span>
                                </li>
                            ))}
                        </ul>
                    </div>
                </form>
            </div>
        );
    }
}