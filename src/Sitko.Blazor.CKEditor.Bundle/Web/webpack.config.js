/**
 * @license Copyright (c) 2014-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-oss-license
 */

'use strict';

/* eslint-env node */

const path = require('path');
const webpack = require('webpack');
const {bundler, styles} = require('@ckeditor/ckeditor5-dev-utils');
const CKEditorWebpackPlugin = require('@ckeditor/ckeditor5-dev-webpack-plugin');
const TerserWebpackPlugin = require('terser-webpack-plugin');

const config = {
  performance: {hints: false},
  entry: {
    "ckeditor": path.resolve(__dirname, 'src', 'ckeditor.js'),
    "ckeditor.dark": path.resolve(__dirname, 'src', 'ckeditor.dark.js')
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
    new CKEditorWebpackPlugin({
      // UI language. Language codes follow the https://en.wikipedia.org/wiki/ISO_639-1 format.
      // When changing the built-in language, remember to also change it in the editor's configuration (src/ckeditor.js).
      language: 'en',
      addMainLanguageTranslationsToAllAssets: true,
      additionalLanguages: 'all'
    }),
    new webpack.BannerPlugin({
      banner: bundler.getLicenseBanner(),
      raw: true
    })
  ],

  module: {
    rules: [
      {
        test: /\.svg$/,
        use: ['raw-loader']
      },
      {
        test: /\.css$/,
        use: [
          {
            loader: 'style-loader',
            options: {
              injectType: 'singletonStyleTag',
              attributes: {
                'data-cke': true
              }
            }
          },
          {
            loader: 'css-loader'
          },
          {
            loader: 'postcss-loader',
            options: {
              postcssOptions: styles.getPostCssConfig({
                themeImporter: {
                  themePath: require.resolve('@ckeditor/ckeditor5-theme-lark')
                },
                minify: true
              })
            }
          },
        ]
      }
    ]
  }
};

module.exports = (env, argv) => {
  if (argv.mode === 'development') {
    config.devtool = 'source-map';
  }

  if (argv.mode === 'production') {
    config.optimization = {
      minimizer: [
        new TerserWebpackPlugin({
          sourceMap: true,
          terserOptions: {
            output: {
              // Preserve CKEditor 5 license comments.
              comments: /^!/
            }
          },
          extractComments: false
        })
      ]
    }
  }


  return config;
};
