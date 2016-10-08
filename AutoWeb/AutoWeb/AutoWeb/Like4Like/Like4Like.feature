Feature: Tự động like kiếm điểm trên like4like.org

@mytag
Scenario: Auto like in like4like
Given Open url "http://www.like4like.org/user/login.php"
And Enter username "daubung08" and pass "Tinhlagio89"
And Click submit
And Wait to login success
And Open url "http://www.like4like.org/free-facebook-likes.php"
And Process like

 