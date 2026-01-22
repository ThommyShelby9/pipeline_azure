import js from '@eslint/js'
import globals from 'globals'
import react from 'eslint-plugin-react'
import reactHooks from 'eslint-plugin-react-hooks'
import reactRefresh from 'eslint-plugin-react-refresh'
import tseslint from 'typescript-eslint'

export default tseslint.config(
  { ignores: ['dist'] },
  {
    extends: [js.configs.recommended, ...tseslint.configs.recommended],
    files: ['**/*.{ts,tsx}'],
    languageOptions: {
      ecmaVersion: 2020,
      globals: globals.browser,
    },
    plugins: {
      'react': react,
      'react-hooks': reactHooks,
      'react-refresh': reactRefresh,
    },
    rules: {
      ...react.configs.recommended.rules,
      ...react.configs['jsx-runtime'].rules,
      ...reactHooks.configs.recommended.rules,
      'react/jsx-no-target-blank': 'off',
      'react-refresh/only-export-components': [
        'warn',
        { allowConstantExport: true },
      ],
      // Custom rules for better code quality
      '@typescript-eslint/no-unused-vars': ['error', { argsIgnorePattern: '^_', varsIgnorePattern: '^_' }],
      '@typescript-eslint/no-explicit-any': 'off', // Allow any for flexibility
      '@typescript-eslint/no-empty-object-type': 'off', // Allow empty interfaces for extensibility
      'react/prop-types': 'off', // Using TypeScript for props validation
      'react/display-name': 'off', // Not needed with arrow functions
      'react/no-unescaped-entities': 'off', // Allow apostrophes in JSX text
      'no-console': 'off', // Allow console for debugging
      'no-useless-escape': 'off', // Allow escaped quotes for clarity
      'prefer-const': 'error',
      'no-var': 'error',
      // Performance rules - disabled as too strict for modern React with hooks
      'react/jsx-no-bind': 'off',
      'react-hooks/exhaustive-deps': 'warn',
    },
    settings: {
      react: {
        version: 'detect',
      },
    },
  },
)