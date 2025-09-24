/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-oss-license
 */

// The editor creator to use.
import {
  ClassicEditor as ClassicEditorBase,
  Alignment,
  Autoformat,
  AutoLink,
  Bold,
  Code,
  CodeBlock,
  Essentials,
  GeneralHtmlSupport,
  Heading,
  HorizontalLine,
  Indent,
  Italic,
  Link,
  List,
  Paragraph,
  RemoveFormat,
  SourceEditing,
  SpecialCharacters,
  SpecialCharactersArrows,
  SpecialCharactersCurrency,
  SpecialCharactersEssentials,
  SpecialCharactersText,
  Strikethrough,
  Subscript,
  Superscript,
  Table,
  TableToolbar,
  TextTransformation,
  Underline,
  WordCount,
} from 'ckeditor5';

import enTranslations from 'ckeditor5/translations/en.js';
import ruTranslations from 'ckeditor5/translations/ru.js';
import 'ckeditor5/ckeditor5.css';

export default class BlazorEditor extends ClassicEditorBase {
  // Plugins to include in the build.
  static builtinPlugins = [
    Alignment,
    Autoformat,
    AutoLink,
    Bold,
    Code,
    CodeBlock,
    Essentials,
    GeneralHtmlSupport,
    Heading,
    HorizontalLine,
    Indent,
    Italic,
    Link,
    List,
    Paragraph,
    RemoveFormat,
    SourceEditing,
    SpecialCharacters,
    SpecialCharactersArrows,
    SpecialCharactersCurrency,
    SpecialCharactersEssentials,
    SpecialCharactersText,
    Strikethrough,
    Subscript,
    Superscript,
    Table,
    TableToolbar,
    TextTransformation,
    Underline,
    WordCount
  ];
  // Editor configuration.
  static defaultConfig = {
    toolbar: {
      items: [
        'sourceEditing',
        '|',
        'undo',
        'redo',
        '|',
        'heading',
        '|',
        'bold',
        'italic',
        'strikethrough',
        'underline',
        'subscript',
        'superscript',
        'alignment',
        '|',
        'bulletedList',
        'numberedList',
        '|',
        'insertTable',
        'link',
        'code',
        'codeBlock',
        'horizontalLine',
        'specialCharacters',
        '|',
        'removeFormat'
      ]
    },
    table: {
      contentToolbar: [
        'tableColumn',
        'tableRow',
        'mergeTableCells'
      ]
    },
    translations: [
      ruTranslations, enTranslations
    ]
  }
}
