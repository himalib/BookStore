
    module.exports = {
    entry: './scripts/index.js',
        output: {
        filename: './scripts/dist/bundle.js'
    },
    devtool: 'source-map',
        module: {
        loaders: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: 'babel-loader',
                query: {
                    presets: ['es2015', 'stage-0']
                }
            }
        ]
    }
};