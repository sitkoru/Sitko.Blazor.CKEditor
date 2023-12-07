/**
 * @license Copyright (c) 2014-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-oss-license
 */

'use strict';

/* eslint-env node */

const path = require('path');
const {CKEditorTranslationsPlugin} = require('@ckeditor/ckeditor5-dev-translations');
const MiniCssExtractPlugin = require( 'mini-css-extract-plugin' );
const {styles} = require('@ckeditor/ckeditor5-dev-utils');

module.exports = {
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
    new CKEditorTranslationsPlugin({
      // See https://ckeditor.com/docs/ckeditor5/latest/features/ui-language.html
      language: 'en',
      addMainLanguageTranslationsToAllAssets: true,
      additionalLanguages: 'all'
    }),
    new MiniCssExtractPlugin( {
      filename: '[name].css'
    } )
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
          }
        ]
      }
    ]
  }
};

// const path = require('path');
// const webpack = require('webpack');
// const {bundler, styles} = require('@ckeditor/ckeditor5-dev-utils');
// const { CKEditorTranslationsPlugin } = require( '@ckeditor/ckeditor5-dev-translations' );
// const TerserWebpackPlugin = require('terser-webpack-plugin');


// const config = {
//


//
//   plugins: [
//     new CKEditorWebpackPlugin({

//     }),
//     new webpack.BannerPlugin({
//       banner: bundler.getLicenseBanner(),
//       raw: true
//     })
//   ],
//
//   module: {
//     rules: [
//       {
//         test: /\.svg$/,
//         use: ['raw-loader']
//       },
//       {
//         test: /\.css$/,
//         use: [
//           {
//             loader: 'style-loader',
//             options: {
//               injectType: 'singletonStyleTag',
//               attributes: {
//                 'data-cke': true
//               }
//             }
//           },
//           {
//             loader: 'css-loader'
//           },
//           {
//             loader: 'postcss-loader',
//             options: {
//               postcssOptions: styles.getPostCssConfig({
//                 themeImporter: {
//                   themePath: require.resolve('@ckeditor/ckeditor5-theme-lark')
//                 },
//                 minify: true
//               })
//             }
//           },
//         ]
//       }
//     ]
//   }
// };
//
// module.exports = (env, argv) => {
//   if (argv.mode === 'development') {
//     config.devtool = 'source-map';
//   }
//
//   if (argv.mode === 'production') {
//     config.optimization = {
//       minimizer: [
//         new TerserWebpackPlugin({
//           sourceMap: true,
//           terserOptions: {
//             output: {
//               // Preserve CKEditor 5 license comments.
//               comments: /^!/
//             }
//           },
//           extractComments: false
//         })
//       ]
//     }
//   }
//
//
//   return config;
// };
