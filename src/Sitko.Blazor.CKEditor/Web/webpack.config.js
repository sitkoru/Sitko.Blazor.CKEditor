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

const mainConfig = {
    performance: {hints: false},
    entry: {
        "Sitko.Blazor.CKEditor": path.resolve(__dirname, 'src', 'main.js'),
    },
    output: {
        path: path.resolve(__dirname, '..', 'wwwroot'),
        filename: '[name].js',
        libraryTarget: 'umd',
        libraryExport: 'default'
    },
};

const ckeConfig = {
    performance: {hints: false},

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
            language: 'ru',
            addMainLanguageTranslationsToAllAssets: true,
            additionalLanguages: []
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
                    'css-loader',
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
    let config;
    switch (env.target) {
        case 'main':
            config = mainConfig;
            break;
        case 'ckeditor':
            config = ckeConfig;
            config.entry = {
                "ckeditor": path.resolve(__dirname, 'src', 'ckeditor.js')
            };
            break;
        case 'ckeditor.dark':
            config = ckeConfig;
            config.entry = {
                "ckeditor.dark": path.resolve(__dirname, 'src', 'ckeditor.dark.js')
            };
            break;
    }

    if (argv.mode === 'development') {
        config.devtool = 'source-map';
    }

    if (argv.mode === 'production') {
        config.optimization = {
            minimize: true,
            minimizer: [
                new TerserWebpackPlugin({
                    terserOptions: {
                        compress: {
                            drop_console: true, // will remove console.logs from your files
                        },
                        output: {
                            // Preserve CKEditor 5 license comments.
                            comments: /^!/
                        },
                    },
                    extractComments: false
                }),
            ],
        }
    }


    return config;
};
