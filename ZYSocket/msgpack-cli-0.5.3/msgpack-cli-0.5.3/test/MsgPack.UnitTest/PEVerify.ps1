foreach( $file in dir MsgPack.Serialization.GeneratedSerealizers*.dll )
{
	$result = ( PEVerify.exe $file /quiet )
	Write-Host $result
	if( !$result.EndsWith( "�p�X" ) )
	{
		$result = ( PEVerify.exe $file /verbose )
		Write-Warning ( [String]::Join( [Environment]::NewLine, $result ) )
	}
}