Public Class LineLib
    'Following code convert from C++
    'Original can be found at https://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/

    'Given three colinear points p, q, r, the function checks if
    'point q lies on line segment 'pr'
    Public Shared Function OnSegment(p As Point, q As Point, r As Point) As Boolean
        If (q.X <= Math.Max(p.X, r.X) And q.X >= Math.Min(p.X, r.X) And q.Y <= Math.Max(p.Y, r.Y) And q.Y >= Math.Min(p.Y, r.Y)) Then Return True
        Return False
    End Function

    'To find orientation of ordered triplet (p, q, r).
    'The function returns following values
    '0 --> p, q And r are colinear
    '1 --> Clockwise
    '2 --> Counterclockwise
    Public Shared Function Orientation(p As Point, q As Point, r As Point) As Integer
        'See https://www.geeksforgeeks.org/orientation-3-ordered-points/
        'for details of below formula.
        Dim val As Integer = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y)

        If (val = 0) Then Return 0  'colinear

        If (val > 0) Then 'clock Or counterclock wise
            Return 1
        Else
            Return 2
        End If
    End Function

    'The main function that returns true if line segment 'p1q1'
    'And 'p2q2'intersect.
    Public Shared Function IsIntersecting(p1 As Point, q1 As Point, p2 As Point, q2 As Point) As Boolean
        'Find the four orientations needed for general And
        'special cases
        Dim o1 As Integer = Orientation(p1, q1, p2)
        Dim o2 As Integer = Orientation(p1, q1, q2)
        Dim o3 As Integer = Orientation(p2, q2, p1)
        Dim o4 As Integer = Orientation(p2, q2, q1)

        'General case
        If ((Not o1 = o2) And (Not o3 = o4)) Then Return True

        'Special Cases
        'p1, q1 And p2 are colinear And p2 lies on segment p1q1
        If (o1 = 0 And OnSegment(p1, p2, q1)) Then Return True

        'p1, q1 And q2 are colinear And q2 lies on segment p1q1
        If (o2 = 0 And OnSegment(p1, q2, q1)) Then Return True

        'p2, q2 And p1 are colinear And p1 lies on segment p2q2
        If (o3 = 0 And OnSegment(p2, p1, q2)) Then Return True

        'p2, q2 And q1 are colinear And q1 lies on segment p2q2
        If (o4 = 0 And OnSegment(p2, q1, q2)) Then Return True

        Return False 'Doesn't fall in any of the above cases
    End Function
End Class
