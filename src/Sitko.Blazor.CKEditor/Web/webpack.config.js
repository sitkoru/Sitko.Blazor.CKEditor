/**
 * @license Copyright (c) 2014-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-oss-license
 */

'use strict';

/* eslint-env node */

const path = require('path');
const TerserWebpackPlugin = require('terser-webpack-plugin');

const config = {
  performance: {hints: false},
  entry: {
    "Sitko.Blazor.CKEditor": path.resolve(__dirname, 'src', 'main.js'),
  },
  output: {
    path: path.resolve(__dirname, '..', 'wwwroot'),
    filename: '[name].js',
    libraryTarget: 'umd',
    libraryExport: 'default'
  }
};


module.exports = (env, argv) => {
  if (argv.mode === 'development') {
    config.devtool = 'source-map';
  }

  config.resolve = {
    alias: {
      config: path.join(__dirname, 'src', 'config.' + argv.mode)
    }
  };

  return config;
};
