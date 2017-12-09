var path = require('path');
var webpack = require('webpack');

module.exports = {
    entry: {
        site: [
            './wwwroot/js/site.js',
            './node_modules/jquery/dist/jquery.js',
            './node_modules/jquery-validation/dist/jquery.validate.js',
            './node_modules/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js'
        ]
    },
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'wwwroot/dist/')
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                loader: 'ts-loader',
                exclude: /node_modules/,
            },
            {
                // Exposes jQuery for use outside Webpack build
                test: require.resolve('jquery'),
                use: [{
                    loader: 'expose-loader',
                    options: 'jQuery'
                }, {
                    loader: 'expose-loader',
                    options: '$'
                }]
            }
        ]
    },
    resolve: {
        extensions: [".tsx", ".ts", ".js"]
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery"
        })
    ]
};