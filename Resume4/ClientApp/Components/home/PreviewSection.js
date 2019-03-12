import React, {Component} from 'react';
import PropTypes from 'prop-types';

export default class PreviewSection extends Component  {
  constructor(props) {
    super(props);
  }

  componentDidUpdate(){
    this.refs.test.innerHTML = this.props.previewText;
  }

  render() {
  return (
        <div class="preview-section">
              <h2>Preview</h2>
              <div class="preview-content">
              <span class="cross">X</span>
              <script src="https://cdnjs.cloudflare.com/ajax/libs/marked/0.3.2/marked.min.js"></script>
                <div class="content" ref="test">
                {}
                </div>
              </div>
            </div>
  );
}
}
