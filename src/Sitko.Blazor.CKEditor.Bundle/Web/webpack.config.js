/**
 * @license Copyright (c) 2014-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-oss-license
 */

'use strict';

/* eslint-env node */

const path = require('path');
const MiniCssExtractPlugin = require( 'mini-css-extract-plugin' );
const IgnoreEmitPlugin = require('ignore-emit-webpack-plugin');


module.exports = {
  performance: {hints: false},
  entry: {
    "ckeditor": path.resolve(__dirname, 'src', 'ckeditor.js'),
    "ckeditor.dark": path.resolve(__dirname, 'src', 'ckeditor.dark.css'),
  },
  output: {
    // The name under which the editor will be exported.
    library: 'BlazorEditor',

    path: path.resolve(__dirname, '..', 'wwwroot'),
    filename: '[name].js',
    libraryTarget: 'umd',
    libraryExport: 'default'
  },
  plugins: [
    new MiniCssExtractPlugin( {
      filename: '[name].css'
    } ),
    new IgnoreEmitPlugin('ckeditor.dark.js')
  ],
  module: {
    rules: [
      {
        test: /ckeditor5-[^/\\]+[/\\]theme[/\\]icons[/\\][^/\\]+\.svg$/,
        use: ['raw-loader']
      },
      {
        test: /\.css$/,
        use: [
          MiniCssExtractPlugin.loader,
          'css-loader'
        ]
      }
    ]
  }
};
