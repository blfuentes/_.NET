cd %~dp0
npm i -g npm-check-updates && ncu && ncu -a && rimraf node_modules && npm install && npm shrinkwrap && npm i -D extract-text-webpack-plugin@next && node node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js