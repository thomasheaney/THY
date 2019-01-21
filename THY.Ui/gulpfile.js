/// <binding BeforeBuild='sass' />
var gulp = require('gulp');

// Template related plugins.
var nunjucksRender = require('gulp-nunjucks-render');

// CSS related plugins.
var sass = require('gulp-sass'); // Gulp pluign for Sass compilation.
var minifycss = require('gulp-uglifycss'); // Minifies CSS files.
var autoprefixer = require('gulp-autoprefixer'); // Autoprefixing magic.
var mmq = require('gulp-merge-media-queries'); // Combine matching media queries into one media query definition.

// Image realted plugins.
var imagemin = require('gulp-imagemin'); // Minify PNG, JPEG, GIF and SVG images with imagemin.

// Utility related plugins.
var rename = require('gulp-rename'); // Renames files E.g. style.css -> style.min.css
var sourcemaps = require('gulp-sourcemaps'); // Maps code in a compressed file (E.g. style.css) back to it’s original position in a source file (E.g. structure.scss, which was later combined with other css files to generate style.css)
var lineec = require('gulp-line-ending-corrector'); // Consistent Line Endings for non UNIX systems. Gulp Plugin for Line Ending Corrector (A utility that makes sure your files have consistent line endings)

var projectURL = '../THY.Web/'; // Local project URL

// Style related.
var styleSRC = 'app/assets/scss/styles.scss'; // Path to main .scss file.
var styleDistDestination = 'dist/assets/css/'; // Path to place the compiled CSS file.
var styleWebDestination = projectURL + 'assets/css/'; // Path to place the compiled CSS file.

// Images related.
var imagesSRC = 'app/assets/images/**/*.{png,jpg,gif,svg}'; // Source folder of images which should be optimized.
var imagesDestination = 'dist/assets/images/'; // Destination folder of optimized images. Must be different from the imagesSRC folder.
var imagesWebDestination = projectURL + 'assets/images/';
// Browsers you care about for autoprefixing.
// Browserlist https        ://github.com/ai/browserslist
const AUTOPREFIXER_BROWSERS = [
    'last 2 version',
    '> 1%',
    'ie >= 9',
    'ie_mob >= 10',
    'ff >= 30',
    'chrome >= 34',
    'safari >= 7',
    'opera >= 23',
    'ios >= 7',
    'android >= 4',
    'bb >= 10'
];



/**
 * Task: `templates`.
 *
 * Compiles Nunjucks templates into static HTML files.
 *
 */
gulp.task('templates', function () {
    return gulp.src('app/assets/templates/pages/**/*.+(html|nunjucks)')
        .pipe(nunjucksRender({
            path: ['app/assets/templates']
        }))
        .pipe(gulp.dest('dist/assets'))
});


/**
 * Task: `styles`.
 *
 * Compiles Sass, Autoprefixes it and Minifies CSS.
 *
 * This task does the following:
 *    1. Gets the source scss file
 *    2. Compiles Sass to CSS
 *    3. Writes Sourcemaps for it
 *    4. Autoprefixes it and generates style.css
 *    5. Renames the CSS file with suffix .min.css
 *    6. Minifies the CSS file and generates style.min.css
 *    7. Injects CSS or reloads the browser via browserSync
 */
gulp.task('styles', function () {
    return gulp.src(styleSRC)
        //.pipe( sourcemaps.init() )
        .pipe(sass({
            errLogToConsole: true,
            outputStyle: 'compact',
            //outputStyle: 'compressed',
            // outputStyle: 'nested',
            // outputStyle: 'expanded',
            precision: 10
        }))
        .on('error', console.error.bind(console))
        .pipe(sourcemaps.write({ includeContent: false }))
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(autoprefixer(AUTOPREFIXER_BROWSERS))


        .pipe(sourcemaps.write("./"))
        .pipe(lineec()) // Consistent Line Endings for non UNIX systems.
        .pipe(gulp.dest(styleDistDestination))
        .pipe(gulp.dest(styleWebDestination))

        .pipe(mmq({ log: true })) // Merge Media Queries only for .min.css version.
        .pipe(rename({ suffix: '.min' }))
        .pipe(minifycss({ maxLineLen: 10 }))
        .pipe(lineec()) // Consistent Line Endings for non UNIX systems.
        .pipe(gulp.dest(styleDistDestination))
        .pipe(gulp.dest(styleWebDestination));

});


/**
  * Task: `images`.
  *
  * Minifies PNG, JPEG, GIF and SVG images.
  *
  * This task does the following:
  *     1. Gets the source of images raw folder
  *     2. Minifies PNG, JPEG, GIF and SVG images
  *     3. Generates and saves the optimized images
  *
  * This task will run only once, if you want to run it
  * again, do it with the command `gulp images`.
  */
gulp.task('images', function () {
    gulp.src(imagesSRC)
        .pipe(imagemin({
            progressive: true,
            optimizationLevel: 3, // 0-7 low-high
            interlaced: true,
            svgoPlugins: [{ removeViewBox: true }]
        }))
        .pipe(gulp.dest(imagesDestination))
        .pipe(gulp.dest(imagesWebDestination));
    
});


gulp.task('sass', function () {
    return gulp.src('app/scss/styles.scss')
        .pipe(sass()) // Using gulp-sass
        .pipe(gulp.dest(styleWebDestination));
});