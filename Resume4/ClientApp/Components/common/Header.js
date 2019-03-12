import React from 'react';

export default function Header() {
  return (
    <header>
      <img class="brand-logo"  src="assets\images\bs23.png"/>
      <div class="logo-bar"></div>
      <div class="menu" >
        <ul>
          <li><a href="#">Home</a></li>
          <li><a href="#">Manage Files</a></li>
          <li><a href="#">Search</a></li>
          <li><a href="#">About</a></li>
        </ul>
      </div>
    </header>
  );
}
