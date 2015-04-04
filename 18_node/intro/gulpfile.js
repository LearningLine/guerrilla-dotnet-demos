/// <vs AfterBuild='default' />

var gulp = require('gulp')
	,concat = require('gulp-concat')
	,uglify = require('gulp-uglify')
	,del = require('del')
;

gulp.task('clean',function (cb) {
	del('./public/js', cb);
});

gulp.task('default', ['clean'], function () {
	//console.log('hello Gulp!');
	return gulp.src('./assets/*.js')
		.pipe(uglify())
		.pipe(concat('mycode.js'))
		.pipe(gulp.dest('./public/js'))
	;
});